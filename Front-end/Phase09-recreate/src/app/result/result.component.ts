import { Component, OnInit, Input } from '@angular/core';
import { Document } from './models/document'
@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit {
  @Input()
  public documents: Document[];

  constructor() { }

  ngOnInit(): void {
  }


}
