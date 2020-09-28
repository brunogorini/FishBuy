import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "app-store-order-placed",
  templateUrl: "./store.order-placed.component.html",
  styleUrls: ["./store.order-placed.component.css"],
})
export class StoreOrderPlacedComponent implements OnInit {
  protected orderId: string;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.orderId = sessionStorage.getItem("orderId");
  }
}
