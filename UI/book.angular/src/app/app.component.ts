//Module används för att göra HTTP förfrågningar till backend, Client är den som faktiskt gör dom
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
//Hanterar asynkrona dataflöden, när man hämtar eller uppdaterar
import { Observable } from 'rxjs';
//Formcontrol representerar varje enskilt fält i ett formulär, formgroup hela formuläret.
import { FormControl, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
//ngIf och ngFor, vilka används i HTML-mallarna.
import { CommonModule } from '@angular/common'; 
//används för att transformera data som kommer från backend (t.ex. ta ut en specifik del av svaret).
import { map } from 'rxjs/operators';

interface APIResponse {
  isSuccess: boolean;
  result: Book[]; // Hantera böcker här
  statuscode: number;
  errorMessages: string[];
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    HttpClientModule,  
    ReactiveFormsModule, 
    CommonModule       
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  books$!: Observable<Book[]>;  
  booksForm!: FormGroup;
  selectedBookId: number | null = null; // Justera till number för att matcha backend

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.booksForm = new FormGroup({
      title: new FormControl(''),
      author: new FormControl(''),
      description: new FormControl(''),
      releaseDate: new FormControl(new Date()),
      genre: new FormControl(''),
      isAvailable: new FormControl(false)
    });

    this.books$ = this.getBooks(); // Hämta böcker från API
  }

  // Hämtar böcker genom GET API och mappar från APIResponse
  getBooks(): Observable<Book[]> {
    return this.http.get<APIResponse>('https://localhost:7121/api/books').pipe(
      map(response => response.result) // Mappa till böckerna i "result"
    );
  }

  // Hantera formulärskickning: skapa ny bok eller uppdatera
  onFormSubmit() {
    const bookData = {
      title: this.booksForm.value.title,
      author: this.booksForm.value.author,
      description: this.booksForm.value.description,
      releaseDate: this.booksForm.value.releaseDate,
      genre: this.booksForm.value.genre,
      isAvailable: this.booksForm.value.isAvailable
    };

    if (this.selectedBookId) {
      // PUT - uppdatera en befintlig bok
      console.log('Uppdaterar bok med ID:', this.selectedBookId);
      this.http.put<APIResponse>(`https://localhost:7121/api/book/${this.selectedBookId}`, { id: this.selectedBookId, ...bookData })
        .subscribe({
          next: (response) => {
            if (response.isSuccess) {
              console.log('Bok uppdaterad, svar från server:', response.result);
              this.books$ = this.getBooks(); // Uppdatera listan efter ändring
              this.booksForm.reset();  // Rensa formuläret
              this.selectedBookId = null;  // Återställ vald bok-ID
            } else {
              console.error('Fel vid uppdatering:', response.errorMessages);
            }
          },
          error: (err) => console.error('Error:', err)
        });
    } else {
      // POST - lägg till en ny bok
      console.log('Skapar ny bok');
      this.http.post<APIResponse>('https://localhost:7121/api/book', bookData)
        .subscribe({
          next: (response) => {
            if (response.isSuccess) {
              console.log('Bok tillagd, svar från server:', response.result);
              this.books$ = this.getBooks(); // Uppdatera listan efter tillägg
              this.booksForm.reset();  // Rensa formuläret
            } else {
              console.error('Fel vid skapande:', response.errorMessages);
            }
          },
          error: (err) => console.error('Error:', err)
        });
    }
  }

  onUpdateBook(book: Book) {
    this.selectedBookId = book.id; // Spara valt boks ID för uppdatering
  
    // Formatdatum till "yyyy-MM-dd"
    const formattedDate = new Date(book.releaseDate).toISOString().split('T')[0];
  
    this.booksForm.setValue({
      title: book.title,
      author: book.author,
      description: book.description,
      releaseDate: formattedDate,  // Använd formaterat datum här
      genre: book.genre,
      isAvailable: book.isAvailable
    });
    console.log('Vald bok för uppdatering:', book); // Logga för kontroll
  }

  onDelete(id: number) { 
    this.http.delete<APIResponse>(`https://localhost:7121/api/book/${id}`)
      .subscribe({
        next: (response) => {
          if (response.isSuccess) {
            console.log('Bok borttagen');
            this.books$ = this.getBooks(); // Uppdatera listan efter borttagning
          } else {
            console.error('Fel vid borttagning:', response.errorMessages);
          }
        },
        error: (err) => console.error('Error:', err)
      });
  }

  trackById(index: number, book: Book): number {
    return book.id;
  }
}

interface Book {
  id: number; 
  title: string;
  author: string;
  description: string;
  releaseDate: Date;
  genre: string;
  isAvailable: boolean;
}
