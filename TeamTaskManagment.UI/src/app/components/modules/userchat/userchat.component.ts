import { Component, OnInit } from '@angular/core';
import { ChatMessage } from 'src/app/Models/models.model';
import { ChatService } from 'src/app/Services/chatservice.service';
@Component({
  selector: 'app-userchat',
  templateUrl: './userchat.component.html',
  styleUrls: ['./userchat.component.css']
})
export class UserchatComponent implements OnInit {
  message = '';
  user = 'User';
  messages: ChatMessage[] = [];

  constructor(public chatService: ChatService) { }

  ngOnInit(): void {
    this.chatService.startConnection();
  }
  // loadMessages() {
  //   this.chatService.loadHistory().then((msgs: any) => {
  //     this.messages = msgs;
  //   }).catch(err => console.error('Failed to load chat history:', err));
  // }
  send(): void {
    if (this.message.trim()) {
      debugger;
      const chatMsg: ChatMessage = {
        user: this.user,
        message: this.message,
        timestamp: new Date().toISOString()
      };
      this.chatService.sendMessage(chatMsg);
      //this.loadMessages();
      console.log(this.chatService.messages);
      this.message = '';
    }
  }

}



