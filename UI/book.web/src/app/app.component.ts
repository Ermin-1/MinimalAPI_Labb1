import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; 

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

    this.books$ = this.getBooks();
  }

  // Hämtar böcker genom GET API
  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>('https://localhost:7121/api/books'); // Justera till "/api/books"
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
      this.http.put(`https://localhost:7121/api/book/${this.selectedBookId}`, { id: this.selectedBookId, ...bookData })
        .subscribe({
          next: (response) => {
            console.log('Bok uppdaterad, svar från server:', response);
            this.books$ = this.getBooks(); // Uppdatera listan efter ändring
            this.booksForm.reset();  // Rensa formuläret
            this.selectedBookId = null;  // Återställ vald bok-ID
          },
          error: (err) => console.error('Error:', err)
        });
    } else {
      // POST - lägg till en ny bok
      console.log('Skapar ny bok');
      this.http.post('https://localhost:7121/api/book', bookData) // Justera till "/api/book"
        .subscribe({
          next: () => {
            console.log('Bok tillagd');
            this.books$ = this.getBooks(); // Uppdatera listan efter tillägg
            this.booksForm.reset();  // Rensa formuläret
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
  

  onDelete(id: number) { // Justera till number för att matcha backend
    this.http.delete(`https://localhost:7121/api/book/${id}`) // Justera till "/api/book"
      .subscribe({
        next: () => {
          alert('Bok borttagen');
          this.books$ = this.getBooks(); // Uppdatera listan efter borttagning
        },
        error: (err) => console.error('Error:', err)
      });
  }

  trackById(index: number, book: Book): number { // Justera till number för att matcha backend
    return book.id;
  }
}

interface Book {
  id: number; // Justera till number för att matcha backend
  title: string;
  author: string;
  description: string; // Nytt fält för beskrivning
  releaseDate: Date;   // Justera till releaseDate
  genre: string;
  isAvailable: boolean; // Nytt fält för tillgänglighet
}
