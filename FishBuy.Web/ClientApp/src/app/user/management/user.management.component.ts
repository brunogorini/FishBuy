import { Component, ViewChild } from "@angular/core";
import { User } from "../../model/user";
import { UserService } from "../../services/user/user.service";
import { Router } from "@angular/router";
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { MatSort } from "@angular/material/sort";
import Swal from "sweetalert2";

@Component({
  selector: "app-user-management",
  templateUrl: "./user.management.component.html",
  styleUrls: ["./user.management.component.css"],
})
export class UserManagementComponent {
  protected users: User[];
  protected dataSource: MatTableDataSource<User>;
  protected searchKey: string;
  protected displayedColumns: string[] = [
    "id",
    "email",
    "firstName",
    "lastName",
    "administrator",
    "edit",
    "delete",
  ];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private userService: UserService, private router: Router) {
    this.userService.getAllUsers().subscribe(
      (users) => {
        this.users = users;
        this.resetDataTable();
      },
      (e) => {
        console.log(e.error);
      }
    );
  }

  public addUser() {
    sessionStorage.setItem("userSession", "");
    this.router.navigate(["/user-registration"]);
  }

  public showDialogDelete(user: User) {
    Swal.fire({
      title: `Are you sure you want to delete ${user.email}?`,
      text: "This action cannot be reverted!",
      icon: "question",
      showCancelButton: true,
      confirmButtonColor: "#fd951f",
      cancelButtonColor: "#767676",
      confirmButtonText: "Yes, delete!",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.deleteUser(user);
      }
    });
  }

  public deleteUser(user: User) {
    this.userService.delete(user).subscribe(
      (users) => {
        this.users = users;
        this.resetDataTable();
        Swal.fire({
          title: "Deleted!",
          text: `${user.email} was successfully deleted.`,
          icon: "success",
          confirmButtonColor: "#8257e6",
        });
      },
      (e) => {
        console.log(e.error);
      }
    );
  }

  public editUser(user: User) {
    sessionStorage.setItem("userSession", JSON.stringify(user));
    this.router.navigate(["/user-registration"]);
  }

  public resetDataTable() {
    this.dataSource = new MatTableDataSource<User>(this.users);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.searchKey = "";
  }

  public applyFilter() {
    this.dataSource.filter = this.searchKey.trim().toLowerCase();
  }
}
