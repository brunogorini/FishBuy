import { Component, OnInit } from "@angular/core";
import { Product } from "../../model/product";
import { ProductService } from "../../services/product/product.service";
import { Router } from "@angular/router";
import Swal from "sweetalert2";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: "app-product-registration",
  templateUrl: "./product.registration.component.html",
  styleUrls: ["./product.registration.component.css"],
})
export class ProductRegistrationComponent implements OnInit {
  protected product: Product;
  protected productSession: string;
  protected selectedFile: File;
  protected spinner_activated: boolean;
  protected messages: string[];
  protected productForm: FormGroup;
  protected submitted = false;

  constructor(
    private productService: ProductService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.productSession = sessionStorage.getItem("productSession");
    if (this.productSession) {
      this.product = JSON.parse(this.productSession);
    } else {
      this.product = new Product();
    }
    this.setFormValidation();
  }

  public setFormValidation() {
    this.productForm = this.formBuilder.group({
      productName: [this.product.name, [Validators.required]],
      productDescription: [
        this.product.description,
        [Validators.required, Validators.minLength(50)],
      ],
      productPrice: [this.product.price, Validators.required],
    });
  }

  get productFormControl() {
    return this.productForm.controls;
  }

  public onSubmit() {
    this.submitted = true;
    if (this.productForm.valid) {
      this.product.name = this.productForm.value.productName;
      this.product.description = this.productForm.value.productDescription;
      this.product.price = this.productForm.value.productPrice;
      this.register();
    }
  }

  public register() {
    if (this.selectedFile) {
      this.uploadFile();
    } else {
      this.product.fileName = undefined;
      this.sendProduct();
    }
  }

  public inputChange(files: FileList) {
    this.selectedFile = files.item(0);
    this.fileValidation();
  }

  public uploadFile() {
    this.productService.sendFile(this.selectedFile).subscribe(
      (fileName) => {
        this.product.fileName = fileName;
        this.sendProduct();
      },
      (e) => {
        console.log(e.error);
      }
    );
  }

  public sendProduct() {
    this.activateWait();
    if (!this.productSession) {
      this.productService.register(this.product).subscribe(
        (productJson) => {
          this.messages = [];
          this.router.navigate(["/product-stock-control"]);
        },
        (e) => {
          this.messages = e.error;
        }
      );
    } else {
      this.productService.update(this.product).subscribe(
        (productJson) => {
          this.messages = [];
          this.router.navigate(["/product-stock-control"]);
        },
        (e) => {
          this.messages = e.error;
        }
      );
    }
    this.deactivateWait();
  }

  public fileValidation() {
    var valid = true;
    if (this.selectedFile != undefined) {
      var fileSize = this.selectedFile.size;
      if (fileSize > 512000) {
        this.showDialogValidationFile("File size cannot exceed 500 KB.");
        valid = false;
      }
      var fileType = this.selectedFile.type;
      if (
        !(
          fileType == "image/jpeg" ||
          fileType == "image/png" ||
          fileType == "image/gif"
        )
      ) {
        this.showDialogValidationFile("File type should be JPG, PNG or GIF.");
        valid = false;
      }
    }
    if (!valid) {
      this.selectedFile = undefined;
    }
  }

  public showDialogValidationFile(message: string) {
    Swal.fire({
      title: "File not uploaded!",
      text: message,
      icon: "error",
      confirmButtonColor: "#fd951f",
    });
  }

  public activateWait() {
    this.spinner_activated = true;
  }

  public deactivateWait() {
    this.spinner_activated = false;
  }
}
