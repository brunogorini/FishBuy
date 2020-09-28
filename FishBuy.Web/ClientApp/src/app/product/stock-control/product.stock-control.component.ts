import { Component, ViewChild } from "@angular/core";
import { Product } from "../../model/product";
import { ProductService } from "../../services/product/product.service";
import { Router } from "@angular/router";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { MatSort } from "@angular/material/sort";
import Swal from "sweetalert2";

@Component({
  selector: "app-product-stock-control",
  templateUrl: "./product.stock-control.component.html",
  styleUrls: ["./product.stock-control.component.css"],
})
export class ProductStockControlComponent {
  protected products: Product[];
  protected dataSource: MatTableDataSource<Product>;
  protected searchKey: string;
  protected displayedColumns: string[] = [
    "name",
    "description",
    "price",
    "picture",
    "edit",
    "delete",
  ];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private productService: ProductService, private router: Router) {
    this.productService.getAllProducts().subscribe(
      (products) => {
        this.products = products;
        this.resetDataTable();
      },
      (e) => {
        console.log(e.error);
      }
    );
  }

  public addProduct() {
    sessionStorage.setItem("productSession", "");
    this.router.navigate(["/product-registration"]);
  }

  public showDialogDelete(product: Product) {
    Swal.fire({
      title: `Are you sure you want to delete ${product.name}?`,
      text: "This action cannot be reverted!",
      icon: "question",
      showCancelButton: true,
      confirmButtonColor: "#fd951f",
      cancelButtonColor: "#767676",
      confirmButtonText: "Yes, delete!",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.deleteProduct(product);
      }
    });
  }

  public deleteProduct(product: Product) {
    this.productService.delete(product).subscribe(
      (products) => {
        this.products = products;
        this.resetDataTable();
        Swal.fire({
          title: "Deleted!",
          text: `${product.name} was successfully deleted.`,
          icon: "success",
          confirmButtonColor: "#8257e6",
        });
      },
      (e) => {
        console.log(e.error);
      }
    );
  }

  public editProduct(product: Product) {
    sessionStorage.setItem("productSession", JSON.stringify(product));
    this.router.navigate(["/product-registration"]);
  }

  public resetDataTable() {
    this.dataSource = new MatTableDataSource<Product>(this.products);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.searchKey = "";
  }

  public applyFilter() {
    this.dataSource.filter = this.searchKey.trim().toLowerCase();
  }
}
