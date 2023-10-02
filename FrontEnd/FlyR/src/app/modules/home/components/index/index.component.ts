import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ServiceService } from 'src/app/services/service.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class IndexComponent {
  constructor(private service : ServiceService, private toastService: ToastrService) { }

  from: string = '';
  to: string = '';
  responseFromApi: any;
  flightsList: any[] = [];
  currencyList: any[] = ["USD", "EUR", "COP"];
  key: string = "USD";
  isDisplayed: boolean = false;

  calculateRoute(){
    this.isDisplayed =true;
    this.service.getJourney(this.from, this.to).subscribe((data) => {
      if (data && data.data){
        console.log(data)
        this.responseFromApi = data;
        this.flightsList = data.data.flights;
        this.isDisplayed =false;
      } else{
        this.toastService.error('No available routes', 'Error');
        this.isDisplayed =false;
      }
      });
  }
}
