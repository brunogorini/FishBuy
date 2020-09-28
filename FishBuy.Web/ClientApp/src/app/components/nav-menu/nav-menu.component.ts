import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { UserService } from "../../services/user/user.service";
import { ShoppingCartService } from "../../services/shopping-cart/shopping-cart.service";

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html",
  styleUrls: ["./nav-menu.component.css"],
})
export class NavMenuComponent implements OnInit {
  public shoppingCart: ShoppingCartService;
  public searchKey: string;

  ngOnInit(): void {
    this.shoppingCart = new ShoppingCartService();
  }

  constructor(private router: Router, private userService: UserService) {
    this.searchKey = "";
  }

  get itemsInCart(): number {
    return this.shoppingCart.itemsInCart();
  }

  public userSignedIn(): boolean {
    return this.userService.user_authenticated();
  }

  public userAdmin(): boolean {
    return this.userService.user_admin();
  }

  public signOut() {
    this.userService.clear_session();
    this.refreshPage();
    this.shoppingCart.emptyShoppingCart();
  }

  get user() {
    return this.userService.user;
  }

  public resetSearch() {
    this.searchKey = "";
    sessionStorage.setItem("searchKey", this.searchKey);
    this.refreshPage();
  }

  public filterProducts() {
    sessionStorage.setItem("searchKey", this.searchKey);
    this.searchKey = "";
    this.refreshPage();
  }

  public refreshPage() {
    this.router
      .navigateByUrl("/signin", { skipLocationChange: true })
      .then(() => {
        this.router.navigate(["/"]);
      });
  }
}
