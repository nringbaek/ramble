export class CreateWallModel {
    name: string;
}

export class CreateWallEntryModel {
    entryType: WallEntryType;
    entryContent: string;
}

export class UpdateWallEntryModel {
    entryContent?: string;
    entryTimestamp?: string;
}

export const enum WallEntryType {
    Text = 1,
    Image = 2
}
