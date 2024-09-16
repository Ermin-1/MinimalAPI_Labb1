export interface Book {
  id: number;
  title: string;
  author: string;
  description: string;  // Ny fält för beskrivning
  genre: string;
  releaseDate?: Date;   // Frivilligt fält för utgivningsdatum
  isAvailable: boolean; // Stavningsändring från "IsAvalible" till "isAvailable"
}
