import { Component, OnInit } from "@angular/core";
import { Product } from "../../model/product";
import { Order } from "../../model/order";
import { Router } from "@angular/router";
import { ComboBoxModel } from "src/app/model/components/ComboBox";
import { OrderService } from "src/app/services/order/order.service";
import { OrderStatusEnum } from "src/app/model/components/OrderStatusEnum";

@Component({
  selector: "app-order-details",
  templateUrl: "./order.details.component.html",
  styleUrls: ["./order.details.component.css"],
})
export class OrderDetailsComponent implements OnInit {
  protected order: Order;
  protected orderSession: string;
  protected statusCombo: ComboBoxModel[];
  protected spinner_activated: boolean;

  constructor(private router: Router, private orderService: OrderService) {
    this.orderSession = sessionStorage.getItem("orderSession");
    if (this.orderSession) {
      this.order = JSON.parse(this.orderSession);
      sessionStorage.setItem("orderSession", "");
    } else {
      this.router.navigate(["/order-management"]);
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

  public updateOrderStatus() {
    this.activateWait();
    this.orderService.updateOrderStatus(this.order).subscribe(
      () => {
        this.router.navigate(["/order-management"]);
      },
      (e) => {
        console.log(e.error);
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
