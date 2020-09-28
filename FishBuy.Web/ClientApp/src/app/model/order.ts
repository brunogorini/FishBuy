import { OrderStatusEnum } from "./components/OrderStatusEnum";
import { OrderItem } from "./orderItem";

export class Order {
  public id: number;

  public userId: number;

  public orderDate: Date;
  public deliveryDate: Date;
  public status: OrderStatusEnum;

  public addressNumber: number;
  public address: string;
  public city: string;
  public state: string;
  public postalCode: string;

  public paymentMethodId: number;
  public total: number;

  public orderItems: OrderItem[];

  constructor() {
    this.orderDate = new Date();
    this.orderItems = [];
  }
}
