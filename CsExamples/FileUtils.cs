using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace FileUtils
{
    public class FileHelper
    {
        // 파일을 읽어 key=line, value=line string 인 ConcurrentDirctionary를 반환한다.
        public static async Task<ConcurrentDictionary<long, string>> ReadLines(string fileName)
        {
            var lineDic = new ConcurrentDictionary<long, string>();
            var readLines = Task.Factory.StartNew(() =>
            {
                Parallel.ForEach(File.ReadLines(fileName), (line, _, lineNumber) =>
                {
                    lineDic.TryAdd(lineNumber, line);
                });

            });

            await readLines;
            return lineDic;
        }

        // 파일을 읽어 각 라인을 LineParseFunc 으로 파싱하여 나온 결과를 value로 하는 ConcurrentDictionary를 반환한다.
        public static async Task<ConcurrentDictionary<long, object>> ParseLines(string fileName, Func<long, string, object> LineParseFunc)
        {
            var parsedLineDic = new ConcurrentDictionary<long, object>();
            var readLines = Task.Factory.StartNew(() =>
            {
                Parallel.ForEach(File.ReadLines(fileName), (line, _, lineNumber) =>
                {
                    parsedLineDic.TryAdd(lineNumber, LineParseFunc(lineNumber, line));
                });
            });

            await readLines;
            return parsedLineDic;
        }
    }
}
