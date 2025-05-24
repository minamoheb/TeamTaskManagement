import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/layout/home/home.component';
import { MainComponent } from './components/layout/main/main.component';
const routes: Routes = [


  // { path: '', component: LoginComponent, pathMatch: 'full' },
  // { path: 'login', component: LoginComponent, data: { isLogin: true } },
  { path: 'Home', component: MainComponent, pathMatch: 'full' },
  {
    path: '',
    component: MainComponent,
    children: [

      {
        path: 'data',
        loadChildren: () => import('./components/modules/datamodule.module').then((m) => m.DatamoduleModule),
      },
      {
        path: 'tasks',
        loadChildren: () => import('./components/modules/taskmanagement/taskmodule.module').then((m) => m.TaskmoduleModule),
      },
    ],
  },




]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
