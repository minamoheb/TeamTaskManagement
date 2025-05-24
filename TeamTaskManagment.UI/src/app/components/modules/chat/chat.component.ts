import { Component, OnInit } from '@angular/core';
import { ChatMessage } from 'src/app/Models/models.model';
import { ChatService } from 'src/app/Services/chatservice.service';
@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  //user = 'User' + Math.floor(Math.random() * 1000);
  message = '';
  user = 'Admin';
  messages: ChatMessage[] = [];

  constructor(public chatService: ChatService) { }
  // isChatOpen = false;

  // toggleChat() {
  //   this.isChatOpen = !this.isChatOpen;
  // }
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
