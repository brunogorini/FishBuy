import { Injectable, Inject, OnInit } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Product } from "../../model/product";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class ProductService implements OnInit {
  private baseUrl: string;
  public products: Product[];

  constructor(private http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.products = [];
  }

  get headers() {
    return new HttpHeaders().set("content-type", "application/json");
  }

  public getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl + "api/product");
  }

  //public getProduct(productId: number): Observable<Product> {
  //  return this.http.get<Product>(this.baseUrl + "api/product");
  //}

  public register(product: Product): Observable<Product> {
    return this.http.post<Product>(
      this.baseUrl + "api/product",
      JSON.stringify(product),
      {
        headers: this.headers,
      }
    );
  }

  public update(product: Product): Observable<Product> {
    return this.http.put<Product>(
      this.baseUrl + "api/product",
      JSON.stringify(product),
      {
        headers: this.headers,
      }
    );
  }

  public delete(product: Product): Observable<Product[]> {
    return this.http.delete<Product[]>(
      this.baseUrl + `api/product/${product.id}`,
      {
        headers: this.headers,
      }
    );
  }

  public sendFile(selectedFile: File): Observable<string> {
    const formData: FormData = new FormData();
    formData.append("FileToUpload", selectedFile, selectedFile.name);
    return this.http.post<string>(
      this.baseUrl + "api/product/file-upload",
      formData
    );
  }
}
