export interface Question {
  id: number;
  text: string;
  answers: Answer[];
}

export interface Answer {
  id: number;
  text: string;
}
