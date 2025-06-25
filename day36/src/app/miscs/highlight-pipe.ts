import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: 'highlightSearch',
})
export class HighlightPipe implements PipeTransform
{
    transform(value: string, arg: string) {
        if (!arg)
            return value
        const regex = new RegExp(arg, 'gi');
        const match = value.match(regex);

        if (!match) {
            return value;
        }

        return value.replace(regex, `<span class='highlight'>${match[0]}</span>`);
    }
    
}