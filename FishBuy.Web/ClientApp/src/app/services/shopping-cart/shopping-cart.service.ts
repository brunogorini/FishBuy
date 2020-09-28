import { Product } from "../../model/product";

export class ShoppingCartService {
  private products: Product[] = [];

  public getAllProducts(): Product[] {
    var productLocalStorage = localStorage.getItem("productLocalStorage");
    if (productLocalStorage) {
      return JSON.parse(productLocalStorage);
    }
    return this.products;
  }

  public addProduct(product: Product) {
    this.products = this.getAllProducts();
    var quantity: number = Number(product.quantity);
    var registered: Product = this.products.find((p) => p.id == product.id);
    if (!registered) {
      product.quantity = quantity;
      product.originalPrice = product.price;
      product.price = product.originalPrice * quantity;
      this.products.push(product);
      localStorage.setItem(
        "productLocalStorage",
        JSON.stringify(this.products)
      );
    } else {
      this.products = this.products.filter((p) => p.id != registered.id);
      registered.quantity += quantity;
      registered.price += registered.originalPrice * quantity;
      this.products.push(registered);
      localStorage.setItem(
        "productLocalStorage",
        JSON.stringify(this.products)
      );
    }
  }

  public update(products: Product[]) {
    localStorage.setItem("productLocalStorage", JSON.stringify(products));
  }

  public deleteProduct(product: Product) {
    var productLocalStorage = localStorage.getItem("productLocalStorage");
    if (productLocalStorage) {
      this.products = JSON.parse(productLocalStorage);
      this.products = this.products.filter((p) => p.id != product.id);
      localStorage.setItem(
        "productLocalStorage",
        JSON.stringify(this.products)
      );
    }
  }

  public itemsInCart(): number {
    var productLocalStorage: string = localStorage.getItem(
      "productLocalStorage"
    );
    var quantity = 0;
    if (!productLocalStorage) {
      return quantity;
    }
    var products: Product[] = JSON.parse(productLocalStorage);

    if (!products) {
      return quantity;
    }
    products.forEach((product: Product) => {
      quantity += product.quantity;
    });
    return quantity;
  }

  public shoppingCartHasItems(): boolean {
    var itens = this.getAllProducts();
    return itens.length > 0;
  }

  public emptyShoppingCart() {
    localStorage.setItem("productLocalStorage", "");
  }
}
