import { Component, OnInit } from "@angular/core";
import { ProductService } from "../../services/product/product.service";
import { Product } from "../../model/product";
import { Router } from "@angular/router";
import { ShoppingCartService } from "../../services/shopping-cart/shopping-cart.service";

@Component({
  selector: "app-store-product",
  templateUrl: "./store.product.component.html",
  styleUrls: ["./store.product.component.css"],
})
export class StoreProductComponent implements OnInit {
  protected product: Product;
  protected shoppingCart: ShoppingCartService;

  constructor(private productService: ProductService, private router: Router) {
    this.product = new Product();
  }

  ngOnInit(): void {
    this.shoppingCart = new ShoppingCartService();
    var productDetails = sessionStorage.getItem("productDetails");
    if (productDetails) {
      this.product = JSON.parse(productDetails);
      this.product.quantity = 1;
    }
  }

  public addToCart() {
    this.shoppingCart.addProduct(this.product);
    this.router.navigate(["/store-shopping-cart"]);
  }

  public updateQuantity(quantity: number) {
    if (quantity <= 0) {
      quantity = 1;
    }
    this.product.quantity = quantity;
  }
}
