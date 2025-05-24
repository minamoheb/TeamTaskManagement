import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ChatMessage } from 'src/app/Models/models.model';
import { ChatService } from 'src/app/Services/chatservice.service';
@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  user = 'Admin' + Math.floor(Math.random() * 1000);
  message = '';
  //user = 'Admin';
  messages: ChatMessage[] = [];
  constructor(public translate: TranslateService, public chatService: ChatService) { }
  isChatOpen = false;
  hasNewMessage = false;

  toggleChat() {
    this.isChatOpen = !this.isChatOpen;
    if (this.isChatOpen) this.hasNewMessage = false;
  }
  ngOnInit(): void {
    this.chatService.startConnection();
    this.chatService.newMessage$.subscribe((msg: ChatMessage) => {
      this.messages.push(msg);
      this.isChatOpen = true; // Auto-open chat popup
      this.hasNewMessage = true;

      // Optional: Play sound
      const audio = new Audio('assets/notification.mp3');
      audio.play();
    });
  }
  // loadMessages() {
  //   this.chatService.loadHistory().then((msgs: any) => {
  //     this.messages = msgs;
  //   }).catch(err => console.error('Failed to load chat history:', err));
  // }
  send(): void {
    if (this.message.trim()) {
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
