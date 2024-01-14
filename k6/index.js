import GetAllProducts from './scenarios/get_all_products.js';

import { group } from 'k6';

import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

export function handleSummary(data) {
  return {
    "k6/summary.html": htmlReport(data),
  };
}

export default () => {

  group('Endpoint Get Contacts - API k6', () => {
    GetAllProducts();
  });

}