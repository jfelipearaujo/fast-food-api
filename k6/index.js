import GetAllProducts from './scenarios/get_all_products.js';
import GetAllOrders from './scenarios/get_all_orders.js';

import { group } from 'k6';

import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

export function handleSummary(data) {
  return {
    "k6/summary.html": htmlReport(data),
  };
}

export const options = {
  discardResponseBodies: true,
  scenarios: {
    get_all_products: {
      executor: 'ramping-vus',
      exec: 'get_all_products',
      startVUs: 0,
      stages: [
        { duration: '1s', target: 1 },
        { duration: '15s', target: 15 },
        { duration: '20s', target: 15 },
        { duration: '40s', target: 30 },
        { duration: '20s', target: 15 },
        { duration: '15s', target: 15 },
        { duration: '1s', target: 0 }
      ],
      gracefulRampDown: '5s',
    },
    get_all_orders: {
      executor: 'ramping-vus',
      exec: 'get_all_orders',
      startVUs: 0,
      stages: [
        { duration: '1s', target: 1 },
        { duration: '15s', target: 15 },
        { duration: '20s', target: 15 },
        { duration: '40s', target: 30 },
        { duration: '20s', target: 15 },
        { duration: '15s', target: 15 },
        { duration: '1s', target: 0 }
      ],
      gracefulRampDown: '5s',
    },
  }
}

export function get_all_products() {
  group('Endpoint Get Products - API k6', () => {
    GetAllProducts();
  });
}

export function get_all_orders() {
  group('Endpoint Get Orders - API k6', () => {
    GetAllOrders();
  });
}