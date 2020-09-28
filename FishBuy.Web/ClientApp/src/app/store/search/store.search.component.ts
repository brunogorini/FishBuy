import { Component } from "@angular/core";
import { ProductService } from "../../services/product/product.service";
import { Product } from "../../model/product";
import { Router } from "@angular/router";

@Component({
  selector: "app-store",
  templateUrl: "./store.search.component.html",
  styleUrls: ["./store.search.component.css"],
})
export class StoreSearchComponent {
  protected products: Product[];

  constructor(private productService: ProductService, private router: Router) {
    this.setProducts();
  }

  public setProducts() {
    var searchKey = sessionStorage.getItem("searchKey");
    this.productService.getAllProducts().subscribe(
      (products) => {
        if (!searchKey) {
          this.products = products;
        } else {
          this.products = this.filterProductsBySearchKey(products, searchKey);
          sessionStorage.setItem("searchKey", "");
        }
      },
      (e) => {
        console.log(e.error);
      }
    );
  }

  public filterProductsBySearchKey(products: Product[], searchKey: string) {
    console.log(products);
    return products.filter(
      (product) =>
        product.description.toLowerCase().includes(searchKey.toLowerCase()) ||
        product.name.toLowerCase().includes(searchKey.toLowerCase())
    );
  }

  public goToProduct(product: Product) {
    sessionStorage.setItem("productDetails", JSON.stringify(product));
    this.router.navigate(["/store-product"]);
  }
}
