import { useState } from "react";
import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";

interface SelectorItem {
  id: string;
  text: string;
  styleId?: string;
}

interface SelectorProps {
  items: SelectorItem[];
  mode?: "single" | "multi";
  selectedIds?: string[];
  onSelectionChange?: (selectedIds: string[]) => void;
  className?: string;
}

export function Selector({ items, mode = "single", selectedIds, onSelectionChange, className }: SelectorProps) {
  const [internalSelected, setInternalSelected] = useState<string[]>([]);

  const isControlled = selectedIds !== undefined;
  const selected = isControlled ? selectedIds : internalSelected;

  const handleItemClick = (itemId: string) => {
    let newSelected: string[];

    if (mode === "single") {
      newSelected = selected.includes(itemId) ? [] : [itemId];
    } else {
      newSelected = selected.includes(itemId) ? selected.filter((id) => id !== itemId) : [...selected, itemId];
    }

    if (!isControlled) {
      setInternalSelected(newSelected);
    }
    onSelectionChange?.(newSelected);
  };

  return (
    <div className={cn("w-full space-y-3 p-2")}>
      <div className="flex flex-wrap gap-2 ">
        {items.map((item) => {
          const isSelected = selected.includes(item.id);
          return (
            <Button
              key={item.id}
              variant="outline"
              size="sm"
              onClick={() => handleItemClick(item.id)}
              className={cn(
                "rounded-full px-4 py-2 border-1 border-primary",
                className,
                item.styleId,
                isSelected &&
                  "bg-primary text-primary-foreground hover:bg-primary hover:text-primary-foreground selected",
                !isSelected && "hover:bg-primary hover:text-primary-foreground "
              )}>
              <span className="flex items-center gap-2">{item.text}</span>
            </Button>
          );
        })}
      </div>
    </div>
  );
}
