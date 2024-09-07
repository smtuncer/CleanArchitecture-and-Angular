import { Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { LayoutsComponent } from './Components/layouts/layouts.component';
import { HomeComponent } from './Components/home/home.component';
import { NotFoundComponent } from './Components/not-found/not-found.component';
import { inject } from '@angular/core';
import { AuthService } from './services/auth.service';
import { blogCategoriesComponent } from './Components/blogCategories/blogCategories.component';

export const routes: Routes = [
    {
        path: "login",
        component: LoginComponent
    },
    {
        path: "",
        component: LayoutsComponent,
        canActivateChild: [() => inject(AuthService).isAuthenticated()],
        children: [
            {
                path: "",
                component: HomeComponent
            },
            {
                path: "blogCategories",
                component: blogCategoriesComponent
            }
        ]
    },
    {
        path: "",
        component: NotFoundComponent
    }
];
