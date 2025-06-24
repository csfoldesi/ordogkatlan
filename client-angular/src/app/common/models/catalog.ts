import type { Genre } from "./genre";
import type { Stage } from "./stage";
import type { Village } from "./village";

export type Catalog = {
  villages: Village[];
  stages: Stage[];
  genres: Genre[];
  dates: string[];
};
