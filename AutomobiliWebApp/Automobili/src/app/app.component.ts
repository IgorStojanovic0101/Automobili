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
  title = 'Automobili';

  store = inject(AutoStore);

  displayedColumns: string[] = ['id', 'godinaProizvodnje', 'tipGoriva', 'nazivAutomobila'];

  constructor() { } // Inject your store service

  ngOnInit() {
  }

}
