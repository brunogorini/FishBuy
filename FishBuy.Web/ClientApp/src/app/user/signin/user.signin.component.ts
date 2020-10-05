import { Component, OnInit } from "@angular/core";
import { User } from "src/app/model/user";
import { Router, ActivatedRoute } from "@angular/router";
import { UserService } from "src/app/services/user/user.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: "app-user-signin",
  templateUrl: "./user.signin.component.html",
  styleUrls: ["./user.signin.component.css"],
})
export class UserSigninComponent implements OnInit {
  protected user: User;
  protected returnUrl: string;
  protected messages: string[];
  protected show: boolean = false;
  protected spinner_activated: boolean;
  protected signinForm: FormGroup;
  protected submitted = false;

  constructor(
    private router: Router,
    private activatedRouter: ActivatedRoute,
    private userService: UserService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.returnUrl = this.activatedRouter.snapshot.queryParams["returnUrl"];
    this.user = new User();
    this.setFormValidation();
  }

  public setFormValidation() {
    this.signinForm = this.formBuilder.group({
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required]],
    });
  }

  get signinFormControl() {
    return this.signinForm.controls;
  }

  public onSubmit() {
    this.submitted = true;
    if (this.signinForm.valid) {
      this.user = this.signinForm.value;
      this.signIn();
      this.signinForm.reset();
      this.submitted = false;
    }
  }

  public signIn(): void {
    this.activateWait();
    this.userService.verifyUser(this.user).subscribe(
      (user_json) => {
        this.userService.user = user_json;
        sessionStorage.setItem(
          "authenticationToken",
          this.userService.user.token
        );
        if (this.returnUrl == undefined) {
          this.router.navigate(["/"]);
        } else {
          this.router.navigate([this.returnUrl]);
        }
        this.deactivateWait();
      },
      (err) => {
        this.messages = err.error;
        this.deactivateWait();
      }
    );
  }

  public activateWait() {
    this.spinner_activated = true;
  }

  public deactivateWait() {
    this.spinner_activated = false;
  }
}
