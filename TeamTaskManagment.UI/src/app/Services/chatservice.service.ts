import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ChatMessage } from '../Models/models.model';
import { Subject, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ChatService {
  private hubConnection: signalR.HubConnection;
  public messages: ChatMessage[] = [];
  public newMessage$ = new Subject<ChatMessage>();

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5063/chathub')
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('SignalR connection started.');
        this.listenForMessages();
      })
      .catch(err => console.error('SignalR connection error:', err));
  }

  private listenForMessages(): void {
    this.hubConnection.on('ReceiveMessage', (user: string, message: string) => {
      const newMsg: ChatMessage = { user, message, timestamp: new Date().toISOString() };
      this.messages.push(newMsg);
      this.newMessage$.next(newMsg);
    });
  }

  public sendMessage(message: ChatMessage): void {
    this.hubConnection.invoke('SendMessage', message)
      .catch(err => console.error('Send error:', err));
  }

  public loadHistory(): Observable<ChatMessage[]> {
    return new Observable(observer => {
      this.hubConnection.invoke<ChatMessage[]>('GetMessageHistory')
        .then(history => {
          this.messages = history;
          observer.next(history);
          observer.complete();
        })
        .catch(err => {
          console.error('Failed to get message history', err);
          observer.error(err);
        });
    });
  }
}
