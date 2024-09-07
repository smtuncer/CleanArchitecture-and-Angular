import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HttpService } from '../../services/http.service';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { FormValidateDirective } from 'form-validate-angular';
import { SwalService } from '../../services/swal.service';
import { BlogCategoriesModel } from '../../models/blogCategories.model';

@Component({
  selector: 'app-blogCategories',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, FormValidateDirective],
  templateUrl: './blogCategories.component.html',
  styleUrl: './blogCategories.component.css'
})
export class blogCategoriesComponent implements OnInit {
  blogCategories: BlogCategoriesModel[] = [];
  
  @ViewChild("addModalCloseBtn") addModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel: BlogCategoriesModel = new BlogCategoriesModel();
  updateModel: BlogCategoriesModel = new BlogCategoriesModel();

  search: string = "";

  constructor(
    private http: HttpService,
    private swal: SwalService
  ) { }

  ngOnInit(): void {
    this.getAll();
  }


  getAll(PageNumber: number = 1, PageSize: number = 100, Search: string = "") {
    this.http.post<BlogCategoriesModel[]>("BlogCategories/GetAll", { PageNumber, PageSize, Search }, (res) => {
      this.blogCategories = res.data;
    });
  }
  
  
  add(form: NgForm) {
    if (form.valid) {
      this.http.post<string>("BlogCategories/Create", this.createModel, (res) => {
        this.swal.callToast(res.data, "success");
        this.getAll();
        this.addModalCloseBtn?.nativeElement.click();
        this.createModel = new BlogCategoriesModel();
      });
    }
  }

  delete(id: string, categoryName: string) {
    this.swal.callSwal("Kategoriyi Sil!", `Kategori ${categoryName} silinsin mi?`, () => {
      this.http.post<string>("BlogCategories/DeleteById", { id: id }, (res) => {
        this.swal.callToast(res.data, "info");
        this.getAll();
      })
    })
  }

  get(data: BlogCategoriesModel) {
    this.updateModel = { ...data };
  }

  update(form: NgForm) {
    if (form.valid) {
      this.http.post<string>("BlogCategories/Update", this.updateModel, (res) => {
        this.swal.callToast(res.data, "success");
        this.getAll();
        this.updateModalCloseBtn?.nativeElement.click();
      });
    }
  }
}
