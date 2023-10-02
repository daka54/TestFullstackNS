import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { homeRoutes } from "./modules/home/home.routes";

@NgModule({
    imports: [RouterModule.forChild([
      ...homeRoutes,
    ])],
    exports: [RouterModule]
})

export class RoutesModule {}
