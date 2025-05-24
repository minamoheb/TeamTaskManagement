import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatComponent } from './chat/chat.component';
import { ShareModule } from '../../share/share.module';
import { Routes, RouterModule } from '@angular/router';
import { TaskmoduleModule } from './taskmanagement/taskmodule.module';
import { UserchatComponent } from './userchat/userchat.component';
const routes: Routes = [
  { path: 'chat', component: ChatComponent, data: { breadcrumb: 'Home/Chat' } },
  { path: 'userchat', component: UserchatComponent, data: { breadcrumb: 'Home/userchat' } },
];
@NgModule({
  declarations: [ChatComponent, UserchatComponent],
  imports: [
    CommonModule,
    TaskmoduleModule,
    ShareModule,
    RouterModule.forChild(routes),
  ]
})
export class DatamoduleModule { }
