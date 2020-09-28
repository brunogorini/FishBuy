import { Component, OnInit } from "@angular/core";
import { ShoppingCartService } from "../../services/shopping-cart/shopping-cart.service";
import { Product } from "../../model/product";
import { Order } from "../../model/order";
import { Router } from "@angular/router";
import { ComboBoxModel } from "src/app/model/components/ComboBox";
import { OrderService } from "src/app/services/order/order.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { OrderStatusEnum } from "src/app/model/components/OrderStatusEnum";
import { PaymentMethodEnum } from "src/app/model/components/PaymentMethodEnum ";

@Component({
  selector: "app-store-checkout",
  templateUrl: "./store.checkout.component.html",
  styleUrls: ["./store.checkout.component.css"],
})
export class StoreCheckoutComponent implements OnInit {
  protected shoppingCart: ShoppingCartService;
  protected products: Product[];
  protected order: Order;
  protected subtotal: number;
  protected shipping: number;
  protected total: number;
  protected items: number;
  protected deliveryCombo: ComboBoxModel[];
  protected delivery: number = 0;
  protected spinner_activated: boolean;
  protected checkoutForm: FormGroup;
  protected submitted = false;
  protected PaymentMethodEnum = PaymentMethodEnum;

  constructor(
    private router: Router,
    private orderService: OrderService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.shoppingCart = new ShoppingCartService();
    this.order = new Order();
    this.deliveryCombo = [
      { value: 0, text: "Standard (7 days)" },
      { value: 1, text: "Express (3 days)" },
    ];
    this.products = this.shoppingCart.getAllProducts();
    this.updateSubtotal();
    this.updateShoppingCartSumary(this.delivery);
    this.setFormValidation();
  }

  public setFormValidation() {
    this.checkoutForm = this.formBuilder.group({
      address: ["", [Validators.required]],
      addressNumber: ["", [Validators.required]],
      city: ["", [Validators.required]],
      state: ["", [Validators.required]],
      postalCode: ["", [Validators.required]],
      paymentMethodId: [
        { value: PaymentMethodEnum[1], disabled: true },
        Validators.required,
      ],
      delivery: [this.delivery, Validators.required],
    });
  }

  get checkoutFormControl() {
    return this.checkoutForm.controls;
  }

  public onSubmit() {
    this.submitted = true;
    if (this.checkoutForm.valid) {
      this.placeOrder();
    }
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

  public updateShoppingCartSumary(delivery: number) {
    switch (Number(delivery)) {
      case 0:
        this.shipping = 4.99;
        break;
      case 1:
        this.shipping = 9.99;
        break;
    }
    this.total = this.subtotal + this.shipping;
  }

  public placeOrder() {
    this.activateWait();
    this.orderService.placeOrder(this.createOrder()).subscribe(
      (orderId) => {
        sessionStorage.setItem("orderId", orderId.toString());
        this.products = [];
        this.shoppingCart.emptyShoppingCart();
        this.router.navigate(["/store-order-placed"]);
      },
      (e) => {
        console.log(e.error);
      }
    );
    this.deactivateWait();
  }

  public createOrder(): Order {
    var preOrder = JSON.parse(sessionStorage.getItem("preOrder"));
    this.order.userId = preOrder.userId;
    this.order.orderItems = preOrder.orderItems;

    this.order.addressNumber = this.checkoutForm.value.addressNumber;
    this.order.address = this.checkoutForm.value.address;
    this.order.city = this.checkoutForm.value.city;
    this.order.state = this.checkoutForm.value.state;
    this.order.postalCode = this.checkoutForm.value.postalCode;
    this.order.status = OrderStatusEnum.Placed;

    this.order.paymentMethodId = 1;
    this.order.total = this.total;

    this.delivery = Number(this.checkoutForm.value.delivery);
    this.order.deliveryDate = new Date();
    switch (this.delivery) {
      case 0:
        this.order.deliveryDate.setDate(this.order.deliveryDate.getDate() + 7);
        break;
      case 1:
        this.order.deliveryDate.setDate(this.order.deliveryDate.getDate() + 3);
        break;
    }
    return this.order;
  }

  public activateWait() {
    this.spinner_activated = true;
  }

  public deactivateWait() {
    this.spinner_activated = false;
  }
}
