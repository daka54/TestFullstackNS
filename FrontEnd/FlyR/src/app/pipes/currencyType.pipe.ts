import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'currencyType',
  pure: false
})
export class CurrencyTypePipe implements PipeTransform {

  private currencyList: {[key: string] : number} = {"USD": 1 , "EUR" : 0.95, "COP" : 4077.67};

  transform(value: any, currency: any): any {
    return value * this.currencyList[currency];
  }

}
