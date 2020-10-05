import { Component, OnInit } from "@angular/core";
import { Product } from "../../model/product";
import { Order } from "../../model/order";
import { Router } from "@angular/router";
import { ComboBoxModel } from "src/app/model/components/ComboBox";
import { OrderService } from "src/app/services/order/order.service";
import { OrderStatusEnum } from "src/app/model/components/OrderStatusEnum";

@Component({
  selector: "app-store-order-details",
  templateUrl: "./store.order-details.component.html",
  styleUrls: ["./store.order-details.component.css"],
})
export class StoreOrderDetailsComponent implements OnInit {
  protected order: Order;
  protected orderSession: string;
  protected orderStatus = OrderStatusEnum;
  protected statusCombo: ComboBoxModel[];
  protected spinner_activated: boolean;

  constructor(private router: Router, private orderService: OrderService) {
    this.orderSession = sessionStorage.getItem("orderSession");
    if (this.orderSession) {
      this.order = JSON.parse(this.orderSession);
      sessionStorage.setItem("orderSession", "");
    } else {
      this.router.navigate(["/store-order-history"]);
    }
  }

  ngOnInit(): void {
    this.statusCombo = [
      { value: 0, text: OrderStatusEnum[0] },
      { value: 1, text: OrderStatusEnum[1] },
      { value: 2, text: OrderStatusEnum[2] },
      { value: 3, text: OrderStatusEnum[3] },
      { value: 4, text: OrderStatusEnum[4] },
    ];
  }

  public cancelOrder() {
    this.activateWait();
    this.order.status = OrderStatusEnum.Cancelled;
    this.orderService.updateOrderStatus(this.order).subscribe(
      () => {
        this.deactivateWait();
        this.router.navigate(["/store-order-history"]);
      },
      (e) => {
        console.log(e.error);
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
