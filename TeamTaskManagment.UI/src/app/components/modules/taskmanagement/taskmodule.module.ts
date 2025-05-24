import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { TasklistComponent } from './tasklist/tasklist.component';
import { TaskdetailComponent } from './taskdetail/taskdetail.component';
import { ShareModule } from 'src/app/share/share.module';
import { TranslateService } from '@ngx-translate/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: 'taskmangament', component: TasklistComponent, data: { breadcrumb: 'Home/Modules/Task' } },
];
@NgModule({
  declarations: [TasklistComponent, TaskdetailComponent],
  imports: [
    ShareModule,
    RouterModule.forChild(routes)

  ],
  providers: [
    TranslateService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class TaskmoduleModule { }
