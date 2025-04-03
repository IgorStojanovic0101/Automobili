import { Component, inject } from '@angular/core';
import { AutoStore } from './shared/store/auto.store';
import { Auto } from './shared/viewModels/auto';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent {
UpdateAuto() {
throw new Error('Method not implemented.');
}
  title = 'Automobili';

  store = inject(AutoStore);

  displayedColumns: string[] = ['id', 'godinaProizvodnje', 'tipGoriva', 'nazivAutomobila'];

  constructor() { } // Inject your store service

  ngOnInit() {
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value.toLowerCase();
    this.store.items().filter(auto => 
      auto.NazivAutomobila.toLowerCase().includes(filterValue)
    );
  }

}
