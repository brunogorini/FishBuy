import { Component, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { MatSort } from "@angular/material/sort";
import { Order } from "src/app/model/order";
import { OrderService } from "src/app/services/order/order.service";
import { OrderStatusEnum } from "src/app/model/components/OrderStatusEnum";
import { UserService } from "src/app/services/user/user.service";
import { Product } from "src/app/model/product";

@Component({
  selector: "app-store-order-history",
  templateUrl: "./store.order-history.component.html",
  styleUrls: ["./store.order-history.component.css"],
})
export class StoreOrderHistoryComponent {
  protected orders: Order[];
  protected dataSource: MatTableDataSource<Order>;
  protected searchKey: string;
  protected StatusEnum = OrderStatusEnum;
  protected displayedColumns: string[] = [
    "id",
    "status",
    "orderDate",
    "deliveryAddress",
    "paymentMethod",
    "orderItems",
    "total",
  ];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private userService: UserService,
    private orderService: OrderService,
    private router: Router
  ) {
    this.orderService.getOrdersByUserId(userService.user.id).subscribe(
      (orders) => {
        this.orders = orders;
        this.resetDataTable();
      },
      (e) => {
        console.log(e.error);
      }
    );
  }

  get noOrderToDisplay(): boolean {
    let noOrderToDisplay: boolean = false;
    setTimeout(function () {
      noOrderToDisplay = true;
    }, 3000);
    return noOrderToDisplay;
  }

  public orderItems(products: Product[]): number {
    if (products) {
      return products.reduce((acc, product) => acc + product.quantity, 0);
    }
  }

  public viewOrderDetails(order: Order) {
    sessionStorage.setItem("orderSession", JSON.stringify(order));
    this.router.navigate(["/store-order-details"]);
  }

  public resetDataTable() {
    this.dataSource = new MatTableDataSource<Order>(this.orders);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.searchKey = "";
  }

  public applyFilter() {
    this.dataSource.filter = this.searchKey.trim().toLowerCase();
  }
}
