import * as fs from 'fs';

export class StaticData {
    private readonly pathToStaticData: string = "./src/staticData"

    private availableSkins: number = 0

    initialize() : void {
        const jsonSkins = fs.readFileSync(`${this.pathToStaticData}/SnakeSkins.json`, "utf-8");
        const availableSkinsJson = JSON.parse(jsonSkins);
        this.availableSkins = availableSkinsJson.AvailableSkins;
    }

    getAvailableSkinCount() : number {
        return this.availableSkins;
    }
}