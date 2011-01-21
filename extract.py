# -*- coding: latin-1 -*-

from xml.etree.ElementTree import iterparse
import re

blacklist = ["Amazonas","Sport Club Corinthians Paulista","Espanha","Clube de Regatas do Flamengo","Portugal","Camboja"]

remove_lang = re.compile("(?ix)(?P<lang>\[\[([a-z]{2,3}|bat-smg|simple|be-x-old|zh-min-nan):.+?\]\]\n?)")
remove_file = re.compile("(?ix)(?P<file>\[\[Ficheiro:.+?\]\]\n?)")

em = re.compile("(?ix)'''(?P<t>.+?)'''")# .sub("<h1>\g<file></h1>",_11)
h1 = re.compile("(?ix)=={{(?P<t>.+?)}}==")# .sub("<h2>\g<file></h2>",_14)
h2 = re.compile("(?ix)==(?P<t>.+?)==")# .sub("<h2>\g<file></h2>",_14)
h3 = re.compile("(?ix)===(?P<t>.+?)===")# .sub("<h2>\g<file></h2>",_14)

category = re.compile("(?ix)(?P<all>\[\[Categoria:(?P<cat>.+?)\]\])")
links = re.compile("(?ix)\[\[(?P<t>.+?)\]\]")

urls = re.compile("(?ix)\[(?P<url>http://[^ ]+)\ (?P<text>.+?)]")

remove_metadata = re.compile("(?ix){{[a-zA-Z0-9]+\s+(\|\s*.+\s*=\s*.+\s*)+}}")

directory = r"D:\temp\ptwiki-20101031-pages-articles.xml"
number_of_files = 2000

context = iterparse(directory+r"\ptwiki-20101031-pages-articles.xml", events=("start", "end"))

# turn it into an iterator
context = iter(context)

# get the root element
event, root = context.next()

namespace = "{http://www.mediawiki.org/xml/export-0.4/}"
i = 1

def strip_metadata(text):
    lines = text.split("\n")
    first_line = lines[0]
    while first_line.strip() == "" or (first_line.startswith("{{") and (first_line.endswith("}}") or first_line.endswith("}}."))):
        lines = lines[1:]
        first_line = lines[0]
    first_line = lines[0]
    if first_line.startswith("{{"):
        while not first_line.strip().endswith("}}"):
            lines = lines[1:]
            first_line = lines[0]
        lines = lines[1:]
    return "\n".join(lines)

for event, elem in context:
    if event == "end" and elem.tag == namespace+"page":
        
        title = elem.find(namespace+"title").text
        text =  elem.find(namespace+"revision").find(namespace+"text").text

        if text == None \
                or len(set(title).intersection(":\\/")) > 0 \
                or text[:20].lower().startswith("#redirect") \
                or text[:20].lower().startswith("{{desambigua"): 
            continue
        
        if title in blacklist or len(text) in [154243,25944,82180,33959]:
            continue
        try:
            print i, title.encode("ascii","ignore"), len(text)
            # with file(directory+r"\files\raw_%s.txt" % title, "w") as f:
            #     f.write(text.encode("utf-8"))
            text = remove_metadata.sub("",strip_metadata(text))
            text = remove_lang.sub("",text)
            text = remove_file.sub("",text)
            text = h1.sub("<h1>\g<t></h1>",text)
            text = h3.sub("<h3>\g<t></h3>",text)
            text = h2.sub("<h2>\g<t></h2>",text)
            text = em.sub("<em>\g<t></em>",text)
            cat = category.search(text)
            text = category.sub("",text)

            tags = links.findall(text)
            text = links.sub("\g<t>",text)
            text = urls.sub("<a href='\g<url>'>\g<text></a>",text)
            text = text.replace("\n","<br/>")
            
            with file(directory+r"\files\text_%s.txt" % title, "w") as f:
                f.write(text.encode("utf-8"))
            with file(directory+r"\files\tags_%s.txt" % title, "w") as f:
                f.writelines([t.split("|")[0].encode("utf-8")+"\n" for t in tags])
            with file(directory+r"\files\cat_%s.txt" % title, "w") as f:
                if cat is not None:
                    f.write(cat.groupdict()["cat"].split("|")[0].encode("utf-8"))
                

            i += 1

        except Exception,e:
            print "Error with %s : %s" % (title.encode("ascii","ignore"), e)
            root.clear()
            continue

        if i >= number_of_files: break
        

        root.clear()
