import { Component } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-footer",
  templateUrl: "./footer.component.html",
  styleUrls: ["./footer.component.css"],
})
export class FooterComponent {
  constructor(private router: Router) {}

  public refreshPage() {
    this.router
      .navigateByUrl("/signin", { skipLocationChange: true })
      .then(() => {
        this.router.navigate(["/"]);
      });
  }
}