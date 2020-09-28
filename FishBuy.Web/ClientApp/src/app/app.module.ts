import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule, Routes } from "@angular/router";
import { TruncateModule } from "ng2-truncate";
import { NoopAnimationsModule } from "@angular/platform-browser/animations";
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSortModule } from "@angular/material/sort";
import { AuthInterceptor } from "./services/authentication/auth.interceptor";
import { AuthGuard } from "./services/authentication/auth.guard";
import { AdminGuard } from "./services/authentication/admin.guard";

import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./components/nav-menu/nav-menu.component";
import { FooterComponent } from "./components/footer/footer.component";
import { HomeComponent } from "./home/home.component";
import { UserSigninComponent } from "./user/signin/user.signin.component";
import { UserSignupComponent } from "./user/signup/user.signup.component";
import { UserManagementComponent } from "./user/management/user.management.component";
import { UserRegistrationComponent } from "./user/registration/user.registration.component";
import { ProductStockControlComponent } from "./product/stock-control/product.stock-control.component";
import { ProductRegistrationComponent } from "./product/registration/product.registration.component";
import { OrderManagementComponent } from "./order/management/order.management.component";
import { OrderDetailsComponent } from "./order/details/order.details.component";
import { StoreSearchComponent } from "./store/search/store.search.component";
import { StoreProductComponent } from "./store/product/store.product.component";
import { StoreShoppingCartComponent } from "./store/shopping-cart/store.shopping-cart.component";
import { StoreCheckoutComponent } from "./store/checkout/store.checkout.component";
import { StoreOrderPlacedComponent } from "./store/order-placed/store.order-placed.component";

import { UserService } from "./services/user/user.service";
import { ProductService } from "./services/product/product.service";
import { OrderService } from "./services/order/order.service";
import { StoreAccountComponent } from "./store/account/store.account.component";
import { StoreOrderHistoryComponent } from "./store/order-history/store.order-history.component";
import { StoreOrderDetailsComponent } from "./store/order-details/store.order-details.component";
import { NotFoundComponent } from "./not-found/not-found.component";
import { AboutUsComponent } from "./about-us/about-us.component";
import { ContactUsComponent } from "./contact-us/contact-us.component";
import { PartnersComponent } from "./partners/partners.component";

const routes: Routes = [
  { path: "", component: HomeComponent, pathMatch: "full" },
  { path: "signin", component: UserSigninComponent },
  { path: "signup", component: UserSignupComponent },
  {
    path: "user-management",
    component: UserManagementComponent,
    canActivate: [AdminGuard],
  },
  {
    path: "user-registration",
    component: UserRegistrationComponent,
    canActivate: [AdminGuard],
  },
  {
    path: "product-stock-control",
    component: ProductStockControlComponent,
    canActivate: [AdminGuard],
  },
  {
    path: "product-registration",
    component: ProductRegistrationComponent,
    canActivate: [AdminGuard],
  },
  {
    path: "order-management",
    component: OrderManagementComponent,
    canActivate: [AdminGuard],
  },
  {
    path: "order-details",
    component: OrderDetailsComponent,
    canActivate: [AdminGuard],
  },
  {
    path: "store-account",
    component: StoreAccountComponent,
    canActivate: [AuthGuard],
  },
  {
    path: "store-order-history",
    component: StoreOrderHistoryComponent,
    canActivate: [AuthGuard],
  },
  {
    path: "store-order-details",
    component: StoreOrderDetailsComponent,
    canActivate: [AuthGuard],
  },
  { path: "store-product", component: StoreProductComponent },
  {
    path: "store-shopping-cart",
    component: StoreShoppingCartComponent,
    canActivate: [AuthGuard],
  },
  {
    path: "store-checkout",
    component: StoreCheckoutComponent,
    canActivate: [AuthGuard],
  },
  {
    path: "store-order-placed",
    component: StoreOrderPlacedComponent,
    canActivate: [AuthGuard],
  },
  { path: "about-us", component: AboutUsComponent },
  { path: "contact-us", component: ContactUsComponent },
  { path: "partners", component: PartnersComponent },
  { path: "404", component: NotFoundComponent },
  { path: "**", redirectTo: "/404" },
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FooterComponent,
    HomeComponent,
    UserSigninComponent,
    UserSignupComponent,
    UserManagementComponent,
    UserRegistrationComponent,
    ProductStockControlComponent,
    ProductRegistrationComponent,
    OrderManagementComponent,
    OrderDetailsComponent,
    StoreAccountComponent,
    StoreOrderHistoryComponent,
    StoreOrderDetailsComponent,
    StoreSearchComponent,
    StoreProductComponent,
    StoreShoppingCartComponent,
    StoreCheckoutComponent,
    StoreOrderPlacedComponent,
    NotFoundComponent,
    AboutUsComponent,
    ContactUsComponent,
    PartnersComponent,
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    TruncateModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    NoopAnimationsModule,
  ],
  providers: [
    UserService,
    ProductService,
    OrderService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
