import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { User } from "src/app/model/user";
import { UserService } from "src/app/services/user/user.service";

@Component({
  selector: "app-user-registration",
  templateUrl: "./user.registration.component.html",
  styleUrls: ["./user.registration.component.css"],
})
export class UserRegistrationComponent implements OnInit {
  protected user: User;
  protected userSession: string;
  protected spinner_activated: boolean;
  protected messages: string[];
  protected show: boolean = false;
  protected showConfirm: boolean = false;
  protected userForm: FormGroup;
  protected submitted = false;

  constructor(
    private userService: UserService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.userSession = sessionStorage.getItem("userSession");
    if (this.userSession) {
      this.user = JSON.parse(this.userSession);
    } else {
      this.user = new User();
      this.user.administrator = false;
    }
    this.setFormValidation();
  }

  public setFormValidation() {
    this.userForm = this.formBuilder.group(
      {
        email: [this.user.email, [Validators.required, Validators.email]],
        password: ["", [Validators.minLength(6)]],
        confirmPassword: ["", Validators.required],
        firstName: [this.user.firstName, Validators.required],
        lastName: [this.user.lastName, Validators.required],
        administrator: [this.user.administrator, Validators.required],
      },
      {
        validator: this.mustMatch("password", "confirmPassword"),
      }
    );
  }

  public mustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
        return;
      }

      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }

  get userFormControl() {
    return this.userForm.controls;
  }

  public onSubmit() {
    this.submitted = true;
    if (this.userForm.valid) {
      this.user.email = this.userForm.value.email;
      this.user.password = this.userForm.value.password;
      this.user.firstName = this.userForm.value.firstName;
      this.user.lastName = this.userForm.value.lastName;
      this.user.administrator = this.userForm.value.administrator;
      this.registerUser();
    }
  }

  public registerUser() {
    this.activateWait();
    if (!this.userSession) {
      this.userService.register(this.user).subscribe(
        (userJson) => {
          this.messages = [];
          this.deactivateWait();
          this.router.navigate(["/user-management"]);
        },
        (e) => {
          this.messages = e.error;
          this.deactivateWait();
        }
      );
    } else {
      this.userService.update(this.user).subscribe(
        () => {
          this.messages = [];
          this.deactivateWait();
          this.router.navigate(["/user-management"]);
        },
        (e) => {
          this.messages = e.error;
          this.deactivateWait();
        }
      );
    }
  }

  public activateWait() {
    this.spinner_activated = true;
  }

  public deactivateWait() {
    this.spinner_activated = false;
  }
}
