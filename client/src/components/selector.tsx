import { useState } from "react";
import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";

interface SelectorItem {
  id: string;
  text: string;
}

interface SelectorProps {
  items: SelectorItem[];
  mode?: "single" | "multi";
  selectedIds?: string[];
  onSelectionChange?: (selectedIds: string[]) => void;
  className?: string;
}

export function Selector({ items, mode = "single", selectedIds = [], onSelectionChange, className }: SelectorProps) {
  const [internalSelected, setInternalSelected] = useState<string[]>(selectedIds);

  const selected = selectedIds.length > 0 ? selectedIds : internalSelected;

  const handleItemClick = (itemId: string) => {
    let newSelected: string[];

    if (mode === "single") {
      newSelected = selected.includes(itemId) ? [] : [itemId];
    } else {
      newSelected = selected.includes(itemId) ? selected.filter((id) => id !== itemId) : [...selected, itemId];
    }

    setInternalSelected(newSelected);
    onSelectionChange?.(newSelected);
  };

  return (
    <div className={cn("w-full space-y-3", className)}>
      <div className="flex flex-wrap gap-2">
        {items.map((item) => {
          const isSelected = selected.includes(item.id);
          return (
            <Button
              key={item.id}
              variant="outline"
              size="sm"
              onClick={() => handleItemClick(item.id)}
              className={cn(
                "rounded-full px-4 py-2 border",
                isSelected &&
                  "bg-primary text-primary-foreground shadow-md border-primary hover:bg-primary/90 hover:text-primary-foreground",
                !isSelected && "hover:bg-gray-50 dark:hover:bg-gray-800 border-border"
              )}>
              <span className="flex items-center gap-2">{item.text}</span>
            </Button>
          );
        })}
      </div>
    </div>
  );
}
