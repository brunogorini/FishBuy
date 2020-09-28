import { Component, OnInit } from "@angular/core";
import { UserService } from "../../services/user/user.service";
import { User } from "../../model/user";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: "app-user-signup",
  templateUrl: "./user.signup.component.html",
  styleUrls: ["./user.signup.component.css"],
})
export class UserSignupComponent implements OnInit {
  protected user: User;
  protected spinner_activated: boolean;
  protected messages: string[];
  protected show: boolean = false;
  protected showConfirm: boolean = false;
  protected user_registered: boolean;
  protected signupForm: FormGroup;
  protected submitted = false;

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.user = new User();
    this.setFormValidation();
  }

  public setFormValidation() {
    this.signupForm = this.formBuilder.group(
      {
        email: ["", [Validators.required, Validators.email]],
        password: ["", [Validators.required, Validators.minLength(6)]],
        confirmPassword: ["", Validators.required],
        firstName: ["", Validators.required],
        lastName: ["", Validators.required],
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

  get signupFormControl() {
    return this.signupForm.controls;
  }

  public onSubmit() {
    this.submitted = true;
    if (this.signupForm.valid) {
      this.user.email = this.signupForm.value.email;
      this.user.password = this.signupForm.value.password;
      this.user.firstName = this.signupForm.value.firstName;
      this.user.lastName = this.signupForm.value.lastName;
      this.user.administrator = false;
      this.registerUser();
      this.signupForm.reset();
      this.submitted = false;
    }
  }

  public registerUser() {
    this.activateWait();
    this.userService.register(this.user).subscribe(
      (userJson) => {
        this.user_registered = true;
        this.messages = [];
        document.forms[0].reset();
      },
      (e) => {
        this.messages = e.error;
      }
    );
    this.deactivateWait();
  }

  public activateWait() {
    this.spinner_activated = true;
  }

  public deactivateWait() {
    this.spinner_activated = false;
  }
}
