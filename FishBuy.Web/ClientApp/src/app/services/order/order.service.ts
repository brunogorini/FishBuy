import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Order } from "../../model/order";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class OrderService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  get headers() {
    return new HttpHeaders().set("content-type", "application/json");
  }

  public getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.baseUrl + "api/order");
  }

  public getOrdersByUserId(userId: number): Observable<Order[]> {
    return this.http.get<Order[]>(this.baseUrl + `api/order/${userId}`);
  }

  public placeOrder(order: Order): Observable<number> {
    return this.http.post<number>(
      this.baseUrl + "api/order",
      JSON.stringify(order),
      { headers: this.headers }
    );
  }

  public updateOrderStatus(order: Order) {
    return this.http.put(this.baseUrl + `api/order/${order.id}`, order.status, {
      headers: this.headers,
    });
  }
}
