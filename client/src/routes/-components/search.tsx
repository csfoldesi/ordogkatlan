import { useState } from "react";
import type { Catalog } from "../-models";
import { Selector } from "@/components/selector";
import { format } from "date-fns";
import { hu } from "date-fns/locale";

type SearchProps = {
  catalog?: Catalog;
};

export const Search = ({ catalog }: SearchProps) => {
  const [selectedDates, setSelectedDates] = useState<string[]>([]);
  const [selectedVillages, setSelectedVillages] = useState<string[]>([]);
  const [selectedStages, setSelectedStages] = useState<string[]>([]);

  if (!catalog) return null;

  const filteredStages =
    selectedVillages.length > 0
      ? catalog.stages.filter((stage) => selectedVillages.includes(stage.villageId))
      : [...catalog.stages];

  const onVillageSelectionChange = (villageIds: string[]) => {
    setSelectedVillages(villageIds);
    // if there are selected villages, filter stages based on villageIds
    if (villageIds.length > 0) {
      const stageIds = catalog.stages.filter((stage) => villageIds.includes(stage.villageId)).map((stage) => stage.id);
      setSelectedStages((currentSelected) => currentSelected.filter((stageId) => stageIds.includes(stageId)));
    }
  };

  return (
    <div className="container mx-auto">
      <Selector
        items={catalog.dates.map((date) => ({
          id: date,
          text: format(date, "EEEE (MMM d.)", { locale: hu }),
        }))}
        mode="multi"
        selectedIds={selectedDates}
        onSelectionChange={(selectedIds) => {
          setSelectedDates(selectedIds);
        }}
      />
      <Selector
        items={catalog.villages.map((village) => ({
          id: village.id,
          text: village.name,
        }))}
        mode="multi"
        selectedIds={selectedVillages}
        onSelectionChange={onVillageSelectionChange}
      />
      <Selector
        items={filteredStages.map((stage) => ({
          id: stage.id,
          text: stage.name,
        }))}
        mode="multi"
        selectedIds={selectedStages}
        onSelectionChange={(selectedIds) => {
          setSelectedStages(selectedIds);
        }}
      />
    </div>
  );
};
