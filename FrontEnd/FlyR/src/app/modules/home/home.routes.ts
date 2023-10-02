import { Routes } from "@angular/router";
import { IndexComponent } from "./components/index/index.component";


export const homeRoutes : Routes = [
  {
    path: '',
    component: IndexComponent,
    loadChildren: () => import('./home.module').then(m => m.HomeModule)
  }
]
