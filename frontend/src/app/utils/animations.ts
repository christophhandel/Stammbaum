import {animate, keyframes, query, stagger, style, transition, trigger} from "@angular/animations";

const animationParams = {
  menuWidth: "200px",
  animationStyle: "500ms ease"
};

export let cardAnimation =     trigger('cardAnimation', [
  transition('* => *', [
    query(':enter', style({opacity: 0}), {optional: true}),
    query(':enter', stagger('50ms', [
      animate('.5s ease-in', keyframes([
        style({opacity: 0, transform: 'translateY(-20%)', offset: 0}),
        style({opacity: .5, transform: 'translateY(-10px) scale(1.1)', offset: 0.3}),
        style({opacity: 1, transform: 'translateY(0)', offset: 1}),
      ]))]), {optional: true}),
    // query(':leave', stagger('50ms', [
    //   animate('1s ease-out', keyframes([
    //     style({opacity: 1, transform: 'scale(1.1)', offset: 0}),
    //     style({opacity: .5, transform: 'scale(.5)', offset: 0.3}),
    //     style({opacity: 0, transform: 'scale(0)', offset: 1}),
    //   ]))]), {optional: true})
  ]),
])

export const fadeIn =
  trigger("fadeInAndOut", [
    transition(":enter", [
      style({opacity: 0}),
      animate("300ms", style({opacity: 1}))
    ]),
    // transition(":leave", [
    //   style({opacity: 1}),
    //   animate("300ms", style({opacity: 0}))
    // ])
  ]);
