export interface Book {
    id: number;
    title: string;
    author: string;
    description: string;
    genre: string;
    releaseDate?: Date; // "?" markerar att fältet är valfritt (nullable i C#).
    isAvailable: boolean;
  }
  