import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-search-card',
  templateUrl: './search-card.component.html',
  styleUrls: ['./search-card.component.scss']
})
export class SearchCardComponent implements OnInit {
  @Output()
  public searched = new EventEmitter<string>();

  @Input() public value: string = '';
  constructor() { }

  ngOnInit(): void {
  }

  public checkForEnter(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      this.searched.emit(this.value);
    }
  }
}
