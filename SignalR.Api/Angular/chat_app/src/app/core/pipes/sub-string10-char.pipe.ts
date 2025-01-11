import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'subString10Char'
})
export class SubString10CharPipe implements PipeTransform {

  transform(value: string | null, ...args: unknown[]): string {
    if (!!!value)
      return '';
    if (value.length <= 10)
      return value;
    return value.substring(0, 10) + '...';
  }

}
