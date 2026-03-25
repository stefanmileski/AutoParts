import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

interface Manufacturer {
  id: number;
  name: string;
}

interface CarModel {
  id: number;
  name: string;
  yearInProductionSince: number;
  yearInProductionTill: number | null;
}

interface Message {
  role: 'user' | 'bot';
  text: string;
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  manufacturers: Manufacturer[] = [];
  carModels: CarModel[] = [];

  selectedManufacturerId: number | null = null;
  selectedCarModelId: number | null = null;

  query: string = '';
  messages: Message[] = [];
  loading: boolean = false;

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    this.http.get<Manufacturer[]>('http://localhost:5156/api/manufacturers')
      .subscribe(data => this.manufacturers = data);
  }

  onManufacturerChange() {
    this.selectedCarModelId = null;
    this.carModels = [];

    if (!this.selectedManufacturerId) return;

    this.http.get<CarModel[]>(`http://localhost:5156/api/carmodels?manufacturerId=${this.selectedManufacturerId}`)
      .subscribe(data => this.carModels = data);
  }

  send() {
    if (!this.query.trim() || !this.selectedCarModelId || this.loading) return;

    this.messages.push({ role: 'user', text: this.query });
    this.loading = true;
    const currentQuery = this.query;
    this.query = '';

    this.http.post<{ response: string }>('http://localhost:5156/api/chatbot/query', {
      query: currentQuery,
      carModelId: this.selectedCarModelId
    }).subscribe({
      next: (res) => {
        this.messages.push({ role: 'bot', text: res.response });
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.messages.push({ role: 'bot', text: 'Something went wrong. Please try again.' });
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }
}