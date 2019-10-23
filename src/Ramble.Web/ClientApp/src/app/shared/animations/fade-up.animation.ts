import { trigger, transition, animate, style, query } from '@angular/animations';

export const fadeUpAnimation = trigger('fadeUpAnimation', [
    transition('* => *', [
        query(':enter',
            [style({ opacity: 0, transform: 'translateY(15px)' })],
            { optional: true }
          ),
          query(':leave',
            [
                style({ opacity: 1 }),
                animate('0.2s ease-in-out', style({ opacity: 0, transform: 'translateY(15px)' }))
            ],
            { optional: true }
          ),
          query(':enter',
            [
                style({ opacity: 0 }),
                animate('0.2s ease-in-out', style({ opacity: 1, transform: 'translateY(0)' }))
            ],
            { optional: true }
          )
    ])
]);
