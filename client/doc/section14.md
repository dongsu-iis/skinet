# Section 14 Client Basket

Create module

```bash
cd client/src/app
ng g m basket
cd basket
ng g m basket-routing --flat
ng g c basket --flat --skip-tests
ng g s basket --flat --skip-tests
```

Install libraly

```bash
npm install uuid
```

```ts
import uuid from 'uuid/v4'
// if uuid v7
import {v4 as uuidv4} from 'uuid'
```
