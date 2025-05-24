
export interface ChatMessage {
  user: string;
  message: string;
  timestamp: string;
}
export interface TaskItem {
  id?: number;
  title: string;
  description: string;
  priority: number;
  status: number;
  dueDate: string;
  assignedTo: string;
}

export interface priorityData {
  id: number;
  priorityName: string;
}
export interface statusData {
  id: number;
  statusName: string;
}

