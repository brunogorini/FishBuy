import { Component, OnInit } from "@angular/core";
import { ShoppingCartService } from "../../services/shopping-cart/shopping-cart.service";
import { Product } from "../../model/product";
import { Order } from "../../model/order";
import { UserService } from "../../services/user/user.service";
import { OrderItem } from "../../model/orderItem";
import { Router } from "@angular/router";

@Component({
  selector: "app-store-shopping-cart",
  templateUrl: "./store.shopping-cart.component.html",
  styleUrls: ["./store.shopping-cart.component.css"],
})
export class StoreShoppingCartComponent implements OnInit {
  protected shoppingCart: ShoppingCartService;
  protected products: Product[];
  protected subtotal: number;
  protected items: number;
  protected spinner_activated: boolean;

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.shoppingCart = new ShoppingCartService();
    this.products = this.shoppingCart.getAllProducts();
    this.updateSubtotal();
  }

  public updatePrice(product: Product, quantity: number) {
    if (quantity <= 0) {
      quantity = 1;
      product.quantity = quantity;
    }

    product.price = product.originalPrice * quantity;
    this.shoppingCart.update(this.products);
    this.updateSubtotal();
  }

  public delete(product: Product) {
    this.shoppingCart.deleteProduct(product);
    this.products = this.shoppingCart.getAllProducts();
    this.updateSubtotal();
  }

  public updateSubtotal() {
    this.subtotal = this.products.reduce(
      (acc, product) => acc + product.price,
      0
    );
    this.items = this.products.reduce(
      (acc, product) => acc + product.quantity,
      0
    );
  }

  public checkOut() {
    this.createPreOrder();
    this.router.navigate(["/store-checkout"]);
  }

  public createPreOrder() {
    let preOrder = new Order();
    preOrder.userId = this.userService.user.id;
    this.products = this.shoppingCart.getAllProducts();
    for (let product of this.products) {
      let orderItem = new OrderItem();
      orderItem.productId = product.id;

      if (!product.quantity) {
        product.quantity = 1;
      }
      orderItem.quantity = product.quantity;

      preOrder.orderItems.push(orderItem);
    }

    sessionStorage.setItem("preOrder", JSON.stringify(preOrder));
  }
}
